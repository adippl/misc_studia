;(format t "Running the contents of init.lisp~%")
 
(defun foo ()
    (format t "called foo...~%"))

(defun ch-brt-smpl (x)
    (do
      ((var 3 (+ var 2)))
      ((> var (/ x 2)))
      (if (= 0 (mod x var))
        (return-from ch-brt-smpl t))))
