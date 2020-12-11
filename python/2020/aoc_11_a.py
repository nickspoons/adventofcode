""" Advent of Code, 2020: Day 11, a """

with open(__file__[:-5] + "_input") as f:
    inputs = [list(line.strip()) for line in f]

height, width = len(inputs), len(inputs[0])


def toggle(grid, x, y):
    """ Toggle a cell between "#" and "L" and update its surrounding counts """
    grid[y][x][0] = "L" if grid[y][x][0] == "#" else "#"
    add = 1 if grid[y][x][0] == "#" else -1
    fromrow = y - 1 if y > 0 else 0
    torow = y + 1 if y < height - 1 else height - 1
    fromcol = x - 1 if x > 0 else 0
    tocol = x + 1 if x < width - 1 else width - 1
    for row in range(fromrow, torow + 1):
        for col in range(fromcol, tocol + 1):
            grid[row][col][1] += add


def play(grid):
    """ Perform a single "round" of the seating game on a copy of the grid """
    newgrid = [[cell.copy() for cell in row] for row in grid]
    for y, r in enumerate(grid):
        for x, c in enumerate(r):
            if c[0] == ".":
                continue
            if (c[0] == "L" and c[1] == 0) or (c[0] == "#" and c[1] > 4):
                toggle(newgrid, x, y)
    return newgrid


def run():
    """ Run grid "rounds" until pattern stabilises """
    grid = [[[c, 0] for c in row] for row in inputs]
    seen = set()
    while True:
        grid = play(grid)
        s = "".join(["".join([c[0] for c in row]) for row in grid])
        if s in seen:
            return s.count("#")
        seen.add(s)

    return 0


if __name__ == "__main__":
    print(run())
