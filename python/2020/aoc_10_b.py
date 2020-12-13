""" Advent of Code, 2020: Day 10, b """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def run():
    """ Use the previous-3 jolt indices to count paths """
    jolts = sorted([int(line) for line in inputs])
    paths = [0, 0, 1] + ([0] * jolts[-1])
    for jolt in jolts:
        paths[jolt + 2] = paths[jolt - 1] + paths[jolt] + paths[jolt + 1]

    return paths[-1]


if __name__ == "__main__":
    print(run())
