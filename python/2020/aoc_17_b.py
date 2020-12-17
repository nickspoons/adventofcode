""" Advent of Code, 2020: Day 17, b """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]

SIZE = 50
space = []
extents = [[0, 0], [0, 0], [0, 0], [0, 0]]


def count(x, y, z, w):
    """ Count the active cells in the 80 surrounding cells """
    n = 0
    for xloc in range(x - 1, x + 2):
        for yloc in range(y - 1, y + 2):
            for zloc in range(z - 1, z + 2):
                for wloc in range(w - 1, w + 2):
                    if xloc == x and yloc == y and zloc == z and wloc == w:
                        continue
                    if space[xloc][yloc][zloc][wloc]:
                        n += 1
    return n


def update_extents(x, y, z, w):
    """ If this coordinate is outside the current extents, update them """
    dimensions = [x, y, z, w]
    for i, dim in enumerate(dimensions):
        if dim < extents[i][0]:
            extents[i][0] = dim
        elif dim > extents[i][1]:
            extents[i][1] = dim


def play():
    """ Play single round of CGoL4D """
    space_next = []
    for _ in range(SIZE):
        space_next.append([])
        for _ in range(SIZE):
            space_next[-1].append([])
            for _ in range(SIZE):
                space_next[-1][-1].append([0] * SIZE)
    for x in range(extents[0][0] - 1, extents[0][1] + 2):
        for y in range(extents[1][0] - 1, extents[1][1] + 2):
            for z in range(extents[2][0] - 1, extents[2][1] + 2):
                for w in range(extents[3][0] - 1, extents[3][1] + 2):
                    space_next[x][y][z][w] = space[x][y][z][w]
    for x in range(extents[0][0] - 1, extents[0][1] + 2):
        for y in range(extents[1][0] - 1, extents[1][1] + 2):
            for z in range(extents[2][0] - 1, extents[2][1] + 2):
                for w in range(extents[3][0] - 1, extents[3][1] + 2):
                    surrounding = count(x, y, z, w)
                    if space[x][y][z][w] and surrounding not in [2, 3]:
                        space_next[x][y][z][w] = 0
                    elif not space[x][y][z][w] and surrounding == 3:
                        space_next[x][y][z][w] = 1
                        update_extents(x, y, z, w)
    for x in range(extents[0][0] - 1, extents[0][1] + 2):
        for y in range(extents[1][0] - 1, extents[1][1] + 2):
            for z in range(extents[2][0] - 1, extents[2][1] + 2):
                for w in range(extents[3][0] - 1, extents[3][1] + 2):
                    space[x][y][z][w] = space_next[x][y][z][w]


def run():
    """ Create CGoL4D "board" and run 6 rounds, then count the active cells """
    for _ in range(SIZE):
        space.append([])
        for _ in range(SIZE):
            space[-1].append([])
            for _ in range(SIZE):
                space[-1][-1].append([0] * SIZE)

    start = SIZE // 2 - (len(inputs[0]) // 2)
    extents[0][0] = extents[1][0] = start
    extents[2][0] = extents[2][1] = start
    extents[3][0] = extents[3][1] = start
    for x, line in enumerate(inputs):
        extents[0][1] = start + x
        for y, c in enumerate(list(line)):
            extents[1][1] = start + y
            if c == "#":
                space[x + start][y + start][start][start] = 1

    for _ in range(6):
        play()

    return sum([sum([sum([sum(z) for z in y]) for y in x]) for x in space])


if __name__ == "__main__":
    print(run())
