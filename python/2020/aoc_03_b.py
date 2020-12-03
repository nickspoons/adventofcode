""" Advent of Code, 2020: Day 03, b """

with open(__file__[:-5] + "_input") as f:
    inputs = list(f)
rows = [line.strip() for line in inputs]


def run():
    """ Use a list of [x, y] coordinates to find trees along various angles """
    height = len(rows)
    width = len(rows[0])
    direction_trees = 1
    for direction in [[1, 1], [3, 1], [5, 1], [7, 1], [1, 2]]:
        trees = 0
        x = 0
        for y in range(direction[1], height - 1, direction[1]):
            line = rows[y]
            x += direction[0]
            if x >= width:
                x -= width
            if line[x] == "#":
                trees += 1
        direction_trees *= trees

    return direction_trees


if __name__ == "__main__":
    print(run())
