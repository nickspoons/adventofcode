""" Advent of Code, 2015: Day 07, a """

import re

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]

CONSTANT = 0
AND = 1
OR = 2
NOT = 3
LSHIFT = 4
RSHIFT = 5


def read():
    """ Use regular expressions to read logic gates into a dictionary """
    wires = dict()
    for instruction in inputs:
        [line_in, line_out] = instruction.split(" -> ")
        match = re.match(r"^(\S+)$", line_in)
        if match:
            a = match.group(1)
            a = int(a) if a.isdigit() else a
            wires[line_out] = [CONSTANT, [a]]
            continue
        match = re.match(r"(\S+) AND (\S+)", line_in)
        if match:
            a = match.group(1)
            a = int(a) if a.isdigit() else a
            wires[line_out] = [AND, [a, match.group(2)]]
            continue
        match = re.match(r"(\S+) OR (\S+)", line_in)
        if match:
            wires[line_out] = [OR, [match.group(1), match.group(2)]]
            continue
        match = re.match(r"NOT (\S+)", line_in)
        if match:
            wires[line_out] = [NOT, [match.group(1)]]
            continue
        match = re.match(r"(\S+) LSHIFT (\S+)", line_in)
        if match:
            wires[line_out] = [LSHIFT, [match.group(1), int(match.group(2))]]
            continue
        match = re.match(r"(\S+) RSHIFT (\S+)", line_in)
        if match:
            wires[line_out] = [RSHIFT, [match.group(1), int(match.group(2))]]
            continue
    return wires


def calculate(wire):
    """ Return tuple: (wire is complete, completed value)  """
    if wire[0] == CONSTANT:
        return isinstance(wire[1][0], int), wire[1][0]
    if wire[0] == AND:
        if isinstance(wire[1][0], int) and isinstance(wire[1][1], int):
            return True, wire[1][0] & wire[1][1]
    if wire[0] == OR:
        if isinstance(wire[1][0], int) and isinstance(wire[1][1], int):
            return True, wire[1][0] | wire[1][1]
    if wire[0] == NOT:
        if isinstance(wire[1][0], int):
            return True, 0b1111111111111111 - wire[1][0]
    if wire[0] == LSHIFT:
        if isinstance(wire[1][0], int):
            return True, wire[1][0] << wire[1][1]
    if wire[0] == RSHIFT:
        if isinstance(wire[1][0], int):
            return True, wire[1][0] >> wire[1][1]
    return False, ""


def propagate(wires, line_out, completed):
    """ Find all line_in values and replace them with the completed value """
    for key in wires:
        for i, line_in in enumerate(wires[key][1]):
            if line_in == line_out:
                wires[key][1][i] = completed


def run():
    """ Read wire logic in, then link ouputs to inputs until complete """
    wires = read()

    while len(wires) > 0:
        lines_out = list(wires.keys())
        for line_out in lines_out:
            completed = calculate(wires[line_out])
            if completed[0]:
                if line_out == "a":
                    return completed[1]
                propagate(wires, line_out, completed[1])
                wires.pop(line_out)

    return 0


if __name__ == "__main__":
    print(run())
