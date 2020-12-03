""" Advent of Code, 2020: Day 03, a """

with open(__file__[:-5] + "_input") as f:
    inputs = list(f)
rows = [line.strip() for line in inputs]


def run():
    """ Step through each row and every 3rd column to find collisions """
    trees = 0
    x = 0
    width = len(rows[0])
    for line in rows[1:]:
        x += 3
        if x >= width:
            x -= width
        if line[x] == "#":
            trees += 1

    return trees


if __name__ == "__main__":
    print(run())
