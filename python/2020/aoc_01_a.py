""" Advent of Code, 2020: Day 01, a """

with open(__file__[:-5] + "_input") as f:
    inputs = list(f)
input_ints = [int(line) for line in inputs]


def run():
    """ Use bounded, nested for loops to add values """
    for i, val1 in enumerate(input_ints):
        for val2 in input_ints[i:]:
            if val1 + val2 == 2020:
                return val1 * val2
    return 0


if __name__ == "__main__":
    print(run())
