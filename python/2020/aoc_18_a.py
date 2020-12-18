""" Advent of Code, 2020: Day 18, a """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]


def evaluate(expression):
    """ Perform operations with left-to-right operator precedence """
    parts = expression.split()
    result = int(parts[0])
    for i in range(1, len(parts), 2):
        if parts[i] == "+":
            result += int(parts[i + 1])
        else:
            result *= int(parts[i + 1])
    return result


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
