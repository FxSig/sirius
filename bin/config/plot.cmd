set terminal wxt size 1000,600
set grid
set xlab "Time (sec)"
set ylab "mm, usec, Khz, V, 0xFF, 0xFFFF"
set key font ",10"
plot filename using 1:2 with lines title columnheader, filename using 1:3 with lines title columnheader, filename using 1:4 with lines title columnheader, filename using 1:5 with lines title columnheader
pause -1