""" Advent of Code, 2020: Day 01, b """

with open(__file__[:-5] + "_input") as f:
    inputs = list(f)
input_ints = [int(line) for line in inputs]


def run():
    for i, val1 in enumerate(input_ints):
        for j, val2 in enumerate(input_ints[i:]):
            for val3 in input_ints[j:]:
                if val1 + val2 + val3 == 2020:
                    return val1 * val2 * val3
    return 0


if __name__ == "__main__":
    print(run())
