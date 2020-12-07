""" Advent of Code, 2015: Day 09, a """

import re

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]

steps = dict()


def travel(places, from_place=None):
    """ Recursively find longest distance to each place """
    if len(places) == 1:
        return steps[frozenset([places.copy().pop(), from_place])]
    distances = []
    for place in places:
        step = steps[frozenset([place, from_place])] if from_place else 0
        remaining = places.copy()
        remaining.remove(place)
        trav = travel(remaining, place)
        distances.append(step + trav)

    return max(distances)


def run():
    """ Use recursion to find shortest path through all places """
    places = set()
    for line in inputs:
        m = re.match(r"(\S+) to (\S+) = (\d+)", line)
        steps[frozenset([m.group(1), m.group(2)])] = int(m.group(3))
        places.add(m.group(1))
        places.add(m.group(2))

    return travel(places)


if __name__ == "__main__":
    print(run())
