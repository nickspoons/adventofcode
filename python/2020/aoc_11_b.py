""" Advent of Code, 2020: Day 11, b """

with open(__file__[:-5] + "_input") as f:
    inputs = [list(line.strip()) for line in f]

height, width = len(inputs), len(inputs[0])


def update_count(grid, coord, delta, add):
    """ Sum the deltas to move along an axis to a seat or edge """
    cur_x, cur_y = coord
    while True:
        cur_x += delta[0]
        cur_y += delta[1]
        if not 0 <= cur_x < width or not 0 <= cur_y < height:
            return
        if grid[cur_y][cur_x][0] == ".":
            continue
        grid[cur_y][cur_x][1] += add
        return


def toggle(grid, x, y):
    """ Toggle a cell between "#" and "L" and update its axis counts """
    grid[y][x][0] = "L" if grid[y][x][0] == "#" else "#"
    add = 1 if grid[y][x][0] == "#" else -1
    update_count(grid, (x, y), (-1, -1), add)
    update_count(grid, (x, y), (-1, 0), add)
    update_count(grid, (x, y), (-1, 1), add)
    update_count(grid, (x, y), (0, 1), add)
    update_count(grid, (x, y), (1, 1), add)
    update_count(grid, (x, y), (1, 0), add)
    update_count(grid, (x, y), (1, -1), add)
    update_count(grid, (x, y), (0, -1), add)


def play(grid):
    """ Perform a single "round" of the seating game on a copy of the grid """
    newgrid = [[cell.copy() for cell in row] for row in grid]
    for y, r in enumerate(grid):
        for x, c in enumerate(r):
            if c[0] == ".":
                continue
            if (c[0] == "L" and c[1] == 0) or (c[0] == "#" and c[1] >= 5):
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
