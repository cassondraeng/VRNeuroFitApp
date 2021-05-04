#!/bin/bash

chmod +x "Exergame Data Battery APP/Exergame Data Battery APP.app/Contents/MacOS/Classic Stroop"
xattr -r -d com.apple.quarantine "Exergame Data Battery APP/Exergame Data Battery APP.app/"
