""" Advent of Code, 2015: Day 15, b """

import re

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def score(ingredients, teaspoons):
    """ Find the product of the 500 calory ingredient scores """
    calories = 0
    for j, spoons in enumerate(teaspoons):
        calories += ingredients[j][4] * spoons
    if calories != 500:
        return 0

    product = 1
    for i in range(4):
        prop = 0
        for j, spoons in enumerate(teaspoons):
            prop += ingredients[j][i] * spoons
        if prop <= 0:
            # The final product will be 0, so exit early
            return 0
        product *= prop
    return product


def run():
    """ Enumerate through all measurement combos adding up to 100 """
    ingredients = []
    for line in inputs:
        pattern = r"^\w+: " + ", ".join(5 * [r"\w+ (-?\d+)"])
        match = re.match(pattern, line)
        ingredients.append([int(match.group(i)) for i in range(1, 6)])
    scores = []
    for a in range(101):
        for b in range(101 - a):
            for c in range(101 - a - b):
                scores.append(score(ingredients, [a, b, c, 100 - a - b - c]))
    return max(scores)


if __name__ == "__main__":
    print(run())
