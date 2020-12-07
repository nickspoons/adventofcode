""" Advent of Code, 2020: Day 07, a """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]

bags = dict()


def search(bag):
    """ Recursively search bags """
    if bag not in bags:
        return False
    return any(b == "shiny gold" or search(b) for b in bags[bag])


def run():
    """ Read inputs into a dictionary for recursive searching """
    for line in inputs:
        # Strip the trailing "." and split
        container, rest = line[:-1].split(" contain ")
        # Strip the trailing " bags"
        container = container[:-5]
        contained = []
        for bag in rest.split(", "):
            if bag[:2] != "no":
                # Strip the leading number and the trailing "bags" or " bag"
                contained.append(bag[2:-4].strip())
        bags[container] = contained

    return sum(1 if search(bag) else 0 for bag in bags)


if __name__ == "__main__":
    print(run())
