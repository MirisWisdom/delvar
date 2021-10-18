<html>
    <h1 align='center'>
        delvar
    </h1>
    <p align='center'>
        use columns in delimited data as variable values
    </p>
</html>

## Introduction

This program lets you use delimited columns as variable values. It subtitutes placeholders with data for each line from a file:

```sh
cat data.tsv 
# Alice   Professional Designer   https://domain.tld/image01.png
# Miris   Imposter Programmer     https://domain.tld/image02.png

delvar 'wget $2 -O $0.png && echo "downloaded the picture for $1"'
# wget 'https://domain.tld/image01.png' -O 'Alice'.png && echo "downloaded the picture for 'Professional Designer'"
# wget 'https://domain.tld/image02.png' -O 'Miris'.png && echo "downloaded the picture for 'Imposter Programmer'"
```

## Usage

Simply write out your desired line with placeholders, then add `delvar` at the start of the line. Examples:

| Command                                        | Information                                            |
| ---------------------------------------------- | ------------------------------------------------------ |
| `delvar echo hello \$0`                        | `$0` will be replaced with the value of the 1st column |
| `delvar 'firefox website.com/$1'`              | output: `firefox website.com/'<2nd column value>'`     |
| `delvar --file ~/websites.tsv wget \$0 -O \$1` | use url in the 1st column, and file in the 2nd column  |

Additional options can be passed for `delvar` regarding the input file, delimiter, etc.

```
delvar -f ~/data.tsv -v '#' -d '\t' 'wget #0 -O #1'
       |-----------| |----| |-----| |-------------|
                   |      |       |               +- line with placeholders to replace
                   |      |       +----------------- column delimiter used in the file
                   |      +------------------------- character for variable identifier
                   +-------------------------------- path to the tsv to read data from
```

By default, `delvar` uses the uses the following values:

- default file is `data.tsv`
- variable character is `$`
- delimiter character is (`\t`)

The aforementioned values can be replaced with any other character:

| Parameter                                       | Description                                         |
| ----------------------------------------------- | --------------------------------------------------- |
| -f, --file, --path=VALUE                        | file with tab-delimited data; default = `data.tsv`  |
| -d, --del, --delimiter=VALUE                    | delimiter character; default = `\t`                 |
| -v, --var, --variable, --id, --identifier=VALUE | symbol to use to identify a variable; default = `$` |
| -q, --quote=VALUE                               | symbol to use for value quotation; default = `'`    |

## Notes

`delvar` will not execute any commands. It simply replaces placeholders with data from the given file. If you want to execute the results, pipe the output to your desired shell, e.g.: `delvar echo hello \$0 | bash`

When providing placeholders, please ensure that you escape characters such as `$`, otherwise your shell may unintentionally evaluate them before `delvar` does.