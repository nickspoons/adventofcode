""" Advent of Code, 2015: Day 02, b """

with open(__file__[:-5] + "_input") as f:
    inputs = list(f)


def run():
    """ Find the smallest perimeter, plus volume """
    ribbon = 0
    for box in inputs:
        d = sorted([int(n) for n in box.strip().split('x')])
        ribbon += 2 * sum(d[:-1]) + d[0] * d[1] * d[2]
    return ribbon


if __name__ == "__main__":
    print(run())
