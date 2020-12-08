""" Advent of Code, 2015: Day 11, b """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]

DIGITS = "abcdefghjkmnpqrstuvwxyz"


def validate_doubles(password):
    """ Passwords must contain at least two different, non-overlapping pairs of
    letters, like aa, bb, or zz """
    for i, c in enumerate(password[:-4]):
        if c == password[i + 1]:
            for j, d in enumerate(password[i + 2:-1]):
                j_index = i + 2 + j
                if d == password[j_index + 1]:
                    return True
    return False


def validate_sequence(password):
    """ Passwords must include one increasing straight of at least three
    letters, like abc, bcd, cde, and so on, up to xyz """
    for i, c in enumerate(password[:-2]):
        if c == password[i + 1] - 1 == password[i + 2] - 2:
            return True
    return False


def validate(password):
    """ Perform 2 validations (but not "i", "l", "o" which is implicit) """
    return validate_doubles(password) and validate_sequence(password)


def increment(digits, i):
    """ Add one to a number represented as a list of base-23 digits """
    if i < 0:
        return
    digits[i] += 1
    if digits[i] >= 23:
        digits[i] -= 23
        increment(digits, i - 1)


def run():
    """ Validate passwords by converting to base 23 digits and incrementing """
    digits = [DIGITS.index(c) for c in inputs[0]]
    first = True
    while True:
        increment(digits, len(digits) - 1)
        if validate(digits):
            if not first:
                return "".join(DIGITS[i] for i in digits)
            first = False

    return ""


if __name__ == "__main__":
    print(run())
