# Compute dodaj dwie liczby 
      .data
      	arr: .word   0 : 12       # "array" of 12 words to contain fib values
	size: .word  12
head: .asciiz  "tekst wyświetlany \n"
tekst1: .asciiz  "wczytaj liczbe calkowita \n"
tekst2: .asciiz  "wprowadzona liczba to: \n"
		.text
		
	

la   $a0, head  # załądowanie adresu zmiennej tekstowej head do rejestru $a0
li   $v0, 4  # załadowanie numeru usługi (4 - wyprowadzenie na konsolę tekstu ) do rejestru $v0
syscall    # wywołanie funkcji we/wy -wyswietlenie tekstu ze zmiennej head
#----------------------------------------------
li $t0, 5  # załadowanie wartości 5 do rejestru $t0
li  $v0, 1           # service 1 is print integer
add $a0, $t0, $zero  # load desired value into argument register $a0, using pseudo-op
syscall
#----------------------------------------------
la   $a0, tekst1 
li   $v0, 4  # załądowanie numeru usługi (4 - wyprowadzenie na konsolę tekstu ) do rejestru $v0
syscall    # wywołanie funkcji we/wy - wyświetlenie tesktu ze zmiennej tekst1


#----------------------------------------------
li   $v0, 5 # service 1 is read integer - consola w tryb czytania liczby całkowitej
syscall  # wartość wprowadzona z klawiatury została zapisana w rejestrze $v0


	la   $t0, arr        # load address of array
        la   $t5, size 
        lw   $t5, 0($t5)
        sw	$v0,0($t0)           
         
         
        
        la   $a0, head
	li   $v0, 4
	syscall
	li $t0, 5
	li  $v0, 1
	add $a0, $t0, $zero
	syscall
	la   $a0, tekst1
	li   $v0, 4
	syscall
	li   $v0, 5
	syscall 
		
	la   $t0, arr        # load address of array
        la   $t5, size 
        lw   $t5, 0($t5)
        sw	$v0,4($t0)  





add $t1, $v0, $zero # załadowanie wartości z rejestru $v0 do rej. $t1
la   $a0, tekst2 # załadowanie adres zmiennej tekst2 do rejestru $a0
li   $v0, 4  # załądowanie numeru usługi (4 - wyprowadzenie na konsolę tekstu ) do rejestru $v0
syscall    # wywołanie funkcji we/wy  - wyświetlenie tekstu ze zmiennej tekst2

li  $v0, 1           # service 1 is print integer - ustawienie funkcji syscall w tryb wyprowadzania liczb całkowitych
add $a0, $t1, $zero  # load desired value into argument register $a0, using pseudo-op
syscall

	lw $v0,0($t0)
	move $v0, $a0
	syscall
	
	
	

add $t1, $v0, $zero # załadowanie wartości z rejestru $v0 do rej. $t1
la   $a0, tekst2 # załadowanie adres zmiennej tekst2 do rejestru $a0
li   $v0, 4  # załądowanie numeru usługi (4 - wyprowadzenie na konsolę tekstu ) do rejestru $v0
syscall    # wywołanie funkcji we/wy  - wyświetlenie tekstu ze zmiennej tekst2

	la   $t0, arr        # load address of array
        la   $t5, size 
        lw   $t5, 0($t5)
        sw	$v0,4($t0) 
        li  $v0, 1           # service 1 is print integer - ustawienie funkcji syscall w tryb wyprowadzania liczb całkowitych
add $a0, $t1, $zero  # load desired value into argument register $a0, using pseudo-op
syscall

add $t1, $v0, $zero # załadowanie wartości z rejestru $v0 do rej. $t1
la   $a0, tekst2 # załadowanie adres zmiennej tekst2 do rejestru $a0
li   $v0, 4  # załądowanie numeru usługi (4 - wyprowadzenie na konsolę tekstu ) do rejestru $v0
syscall    # wywołanie funkcji we/wy  - wyświetlenie tekstu ze zmiennej tekst2

	la   $t0, arr        # load address of array
        la   $t5, size 
        lw   $t5, 0($t5)
        sw	$v0,0($t0) 
        li  $v0, 1           # service 1 is print integer - ustawienie funkcji syscall w tryb wyprowadzania liczb całkowitych
add $a0, $t1, $zero  # load desired value into argument register $a0, using pseudo-op
syscall