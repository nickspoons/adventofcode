""" Advent of Code, 2020: Day 07, a """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]

bags = dict()


def count_bags(bag):
    """ Recursively count bags """
    if bag not in bags:
        return 1
    return 1 + sum(b[0] * count_bags(b[1]) for b in bags[bag])


def run():
    """ Read inputs into a dictionary for recursive searching """
    for line in inputs:
        # Strip the trailing "." and split
        container, rest = line[:-1].split(" contain ")
        if rest[:2] == "no":
            continue
        # Strip the trailing " bags"
        container = container[:-5]
        contained = []
        for part in rest.split(", "):
            # Strip the trailing "bags" or " bag"
            parts = part[:-4].strip().split(" ", 1)
            contained.append([int(parts[0]), parts[1]])
        bags[container] = contained

    return count_bags("shiny gold") - 1


if __name__ == "__main__":
    print(run())
