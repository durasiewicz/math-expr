# math-expr
Just another math expression evaluator.

All calculations are performed using ```decimal``` type. 

Build-in functions:

| FunctionName(arg, ...) |
| --- |
| pow(x, y) |
| sqrt(x) |
| exp(x) |
| log10(x) |
| log(x) |
| cos(x) |
| tan(x) |
| sin(x) |
| sinh(x) |
| cosh(x) |
| tanh(x) |
| abs(x) |
| asin(x) |
| atan(x) |
| atan2(x, y) |
| acos(x) |

Variables are also supported:

```
a = 2
b = pow(2, 2)
c = (a + b) * 2
d = pow(a, sqrt(b))
```

Single-line expressions can be separated with semicolon (only last expression result is printed to the console):

```
a = 2; b = 3
pow(a, b)
```

Bitwise operators are also supported and have lowest precedence:

| Operator | Operation |
|--------|--------|
| `&`    | and    |
| `\|`   |     or |
| `^` | xor |

```
a = 1 & 2
b = 1 * 2 | 1 + 2 * 2
c = a + 1 ^ b / 2
```

**But please be aware, that every value is converted to integer (fractional part is lost) before performing bitwise operation.**
