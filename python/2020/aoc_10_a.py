""" Advent of Code, 2020: Day 10, a """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def run():
    """ Count diffs in sorted adapter levels """
    jolts = [int(line) for line in inputs]
    jolts += [0, max(jolts) + 3]
    jolts = sorted(jolts)
    ones = 0
    threes = 0
    for i in range(len(jolts) - 1):
        diff = jolts[i + 1] - jolts[i]
        if diff == 1:
            ones += 1
        elif diff == 3:
            threes += 1
    return ones * threes


if __name__ == "__main__":
    print(run())
