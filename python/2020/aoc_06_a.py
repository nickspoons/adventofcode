""" Advent of Code, 2020: Day 06, a """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def run():
    """ Sum the union of all character sets in each group """
    groups = []
    group = []
    for line in inputs:
        if line == "":
            groups.append(group)
            group = []
        else:
            group.append(set(line))
    groups.append(group)

    return sum(len(set.union(*group)) for group in groups)


if __name__ == "__main__":
    print(run())
