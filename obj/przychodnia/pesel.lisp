;(format t "Loading pesel.lisp~%")

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

(defun pesel-test (str)
  (let ((digit-list (split-str-to-digit-list str)))
		(if (= 
			  (nth 0 (last digit-list))
			  (pesel-calc-sum digit-list))
		  (return-from pesel-test t)
		  (return-from pesel-test nil))))

(defun pesel-get-gender (str)
  (let ((digit-list (split-str-to-digit-list str)))
	(if
	  (= 1 (rem (nth 10 digit-list) 2))
	  (return-from pesel-get-gender "m")
	  (return-from pesel-get-gender "f"))))

(defun pesel-birth-date (str)
  (let ((char-list (coerce str 'list)))
	(concatenate 'string
		(string (nth 0 char-list))
		(string (nth 1 char-list))
		"-"
		(string (nth 2 char-list))
		(string (nth 3 char-list))
		"-"
		(string (nth 4 char-list))
		(string (nth 5 char-list)))))

(defun pesel-age (str)
  (let ((digit-list (split-str-to-digit-list str)))
	(let ((pesel-year (+ (* 10 (nth 0 digit-list))
						 (nth 1 digit-list)))
		  (month (+ (* 10 (nth 2 digit-list))
					(nth 3 digit-list))))
	  (return-from pesel-age
				 (-
				   (nth 5 (multiple-value-list (get-decoded-time)))
				   (cond
					 ((< month 13) (+ pesel-year 1900))
					 ((< month 33) (+ pesel-year 2000))
					 ((< month 53) (+ pesel-year 2100))
					 ((< month 73) (+ pesel-year 2200))))))))



;(defvar *test-wrong-pesel* "12345678901")
;
;(print (pesel-test "12345678901"))
;(print (pesel-get-gender "12345678901"))
;(print (pesel-birth-date "12345678901"))
;(print (pesel-age *test-wrong-pesel*))
