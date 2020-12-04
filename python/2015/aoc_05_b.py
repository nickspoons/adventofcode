""" Advent of Code, 2015: Day 05, b """

with open(__file__[:-5] + "_input") as f:
    passwords = [p.strip() for p in f]


def validate_pairs(password):
    """
    It contains a pair of any two letters that appears at least twice in the
    string without overlapping, like xyxy (xy) or aabcdefgaa (aa), but not like
    aaa (aa, but it overlaps).
    """
    for i in range(len(password) - 2):
        if password[i:i + 2] in password[i + 2:]:
            return True
    return False


def validate_gap(password):
    """
    It contains at least one letter which repeats with exactly one letter
    between them, like xyx, abcdefeghi (efe), or even aaa.
    """
    for i in range(len(password) - 2):
        if password[i] == password[i + 2]:
            return True
    return False


def run():
    """ Validate passwords in various ways """
    valid = 0
    for password in passwords:
        if validate_pairs(password) \
                and validate_gap(password):
            valid += 1
    return valid


if __name__ == "__main__":
    print(run())
