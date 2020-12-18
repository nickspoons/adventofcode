""" Advent of Code, 2020: Day 18, b """

import re

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def evaluate(expression):
    """ Perform operations with + then * operator precedence """
    sub_add = lambda m: str(int(m.group(1)) + int(m.group(2)))
    while "+" in expression:
        expression = re.sub(r"(\d+) \+ (\d+)", sub_add, expression)
    sub_multiply = lambda m: str(int(m.group(1)) * int(m.group(2)))
    while "*" in expression:
        expression = re.sub(r"(\d+) \* (\d+)", sub_multiply, expression)
    return int(expression)


def parse(line):
    """ Replace sections in parentheses, then evaluate the expression """
    while "(" in line:
        iend = line.find(")")
        istart = line.rfind("(", 0, iend)
        evaluated = evaluate(line[istart + 1:iend])
        line = f"{line[:istart]}{evaluated}{line[iend + 1:]}"
    return evaluate(line)


def run():
    """ Find the sum of the parsed mathematics lines """
    return sum(parse(line) for line in inputs)


if __name__ == "__main__":
    print(run())
