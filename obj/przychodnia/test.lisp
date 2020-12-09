(defun split-str-to-digit-list (str)
  (mapcar #'digit-char-p
	(coerce str 'list )))

(defun pesel-calc-sum (digit-list)
  (mod
	(let ((sum 0))
	  (+ sum
		  (* (nth 0 digit-list) 9)
		  (* (nth 1 digit-list) 7)
		  (* (nth 2 digit-list) 3)
		  (* (nth 3 digit-list) 1)
		  (* (nth 4 digit-list) 9)
		  (* (nth 5 digit-list) 7)
		  (* (nth 6 digit-list) 3)
		  (* (nth 7 digit-list) 1)
		  (* (nth 8 digit-list) 9)
		  (* (nth 9 digit-list) 7)))
	 10))

(defun pesel-test (digit-list)
  (if (= 
		(nth 0 (last digit-list))
		(pesel-calc-sum digit-list))
		
		(return-from pesel-test t)
		(return-from pesel-test nil)))


(print (split-str-to-digit-list "12345678901"))

(print (pesel-calc-sum (split-str-to-digit-list "12345678901")))
(print (pesel-test (split-str-to-digit-list "12345678901")))
