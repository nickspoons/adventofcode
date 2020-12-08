""" Advent of Code, 2015: Day 11, a """

with open(__file__[:-5] + "_input") as f:
    inputs = [line.strip() for line in f]

DIGITS = "abcdefghjkmnpqrstuvwxyz"


def from_base(s, b):
    """ Convert string to number in arbitrary base """
    return 0 if len(s) == 0 else b * from_base(s[:-1], b) + DIGITS.index(s[-1])


def to_base(n, b):
    """ Convert number to string in arbitrary base """
    return "0" if not n else to_base(n // b, b).lstrip("0") + DIGITS[n % b]


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
        if ord(c) == ord(password[i + 1]) - 1 == ord(password[i + 2]) - 2:
            return True
    return False


def validate(password):
    """ Perform 2 validations (but not "i", "l", "o" which is implicit) """
    return validate_doubles(password) and validate_sequence(password)


def run():
    """ Validate passwords by converting to base 23 and incrementing """
    # Since only alphabetic characters but not ["i", "l", "o"] are allowed, use
    # base 23
    old_password = from_base(inputs[0], 23)
    n = 1
    while True:
        password = to_base(old_password + n, 23)
        if validate(password):
            return password
        n += 1

    return ""


if __name__ == "__main__":
    print(run())
