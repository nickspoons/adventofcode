""" Advent of Code, 2015: Day 13, a """

import itertools
import re

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]

scores_a, scores_b = dict(), dict()


def get_score(person_a, person_b):
    """ Get the combined scores of the 2 poeple sitting next to each other """
    return scores_a[person_a][person_b] + scores_b[person_a][person_b]


def run():
    """ Find all permutations of table ordering and calculate the happiest """
    for line in inputs:
        match = re.match(r"^(\w+) would (gain|lose) (\d+) .* (\w+)\.$", line)
        a = match.group(1)
        b = match.group(4)
        score = int(match.group(3))
        if match.group(2) == "lose":
            score = 0 - score
        if a not in scores_a:
            scores_a[a] = dict()
        if b not in scores_b:
            scores_b[b] = dict()
        scores_a[a][b] = score
        scores_b[b][a] = score

    people = len(scores_a)
    best = 0
    for permutation in itertools.permutations(scores_a.keys()):
        score = 0
        for i in range(people - 1):
            score += get_score(permutation[i], permutation[i + 1])
        score += get_score(permutation[0], permutation[-1])
        if score > best:
            best = score

    return best


if __name__ == "__main__":
    print(run())
