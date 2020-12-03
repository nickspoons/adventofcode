""" Advent of Code, 2015: Day 01, a """

with open(__file__[:-5] + "_input") as f:
    inputs = list(f)
floors = inputs[0]


def run():
    """ Find the correct floor by converting to ints and adding """
    return sum([1 if f == '(' else -1 for f in list(floors)])


if __name__ == "__main__":
    print(run())
