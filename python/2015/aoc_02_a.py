""" Advent of Code, 2015: Day 02, a """

with open(__file__[:-5] + "_input") as f:
    inputs = list(f)


def run():
    """ Sum the box sides, plus the smallest side again as extra """
    paper = 0
    for box in inputs:
        d = [int(n) for n in box.strip().split('x')]
        sides = [d[0] * d[1], d[0] * d[2], d[1] * d[2]]
        paper += 2 * sum(sides) + min(sides)
    return paper


if __name__ == "__main__":
    print(run())
