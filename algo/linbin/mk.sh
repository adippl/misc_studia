#!/bin/bash
case $1 in
	"bpdf")
		preconv main.ms |eqn |groff -ms -Tps  |ps2pdf - - > out.pdf
	;;
	 "build")
		echo building... 

	mkdir pic
	mkdir data

	mcs ./main.cs

	for x in 4 8 12 16 20 24 28 30
	do 
	mono main.exe $x 0|tee data/$x.tab
	done

	for x in `ls data/*.tab`
	do
	cat $x| awk '{print $3 " " $4}'|sed s/,\/\./g > `echo $x|cut -f1 -d"."`.time
	done

	for x in `ls data/*.time`
	do
	gnuplot -e "set term png size 640,480; set encoding utf8; set logscale y;  plot '"$x"' using 0:1 with lines title 'binsearch', '' using 0:2 with lines title 'linsearch'" > pic/`echo $x|cut -f1 -d"."|cut -f2 -d"/"`.png
	done

	for x in `ls pic/*.png`
	do
	convert $x eps2:`echo $x|cut -f1 -d"." `.eps
	done


	for x in 4 8 12 16 20 24 28 30
	do 
	mono main.exe $x 1|tee data/$x.mimx
	done


	for x in `seq 2 30`
	do 
	mono main.exe $x 2|tee data/avg.tab
	done

	cat data/avg.tab |sed s/\,/\./ > data/avg.time
	gnuplot -e "set term png size 640,480; set encoding utf8; set logscale y;  plot 'data/avg.time' using 0:1 with lines title 'binsearch', '' using 0:2 with lines title 'linsearch'" > pic/avg.png
	convert pic/avg.png eps2:pic/avg.eps

	preconv main.ms |eqn |groff -ms -Tps  |ps2pdf - - > out.pdf
	
		exit
	;;
	"clean-data")
		echo clean-data
		rm -rf data pic
		exit
	;;
	"clean")
		echo clean
		sh mk.sh clean-data
		rm out.pdf main.exe
		exit
	;;
	*)
		echo error, exit
		exit
	;;
esac
