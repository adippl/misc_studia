(defvar *test* '(100913 1009139 10091401 100914061 1009140611 10091406133 100914061337 1009140613399))
(defvar *ncpu* 4)
(defvar *tl-size* 40000)


(defun genl (n size)
  (let
    ((i (* n size 2))
     (lst '()))
    (loop for s from 1 to size collect
	  (+ i 1 (* s 2)))))

(defun ch-brt-smpl (x)
    (do
      ((var 3 (+ var 2)))
      ((> var (/ x 2)))
      (if (= 0 (mod x var))
        (return-from ch-brt-smpl t))))

(defun ch-brt-thread-old (z) 
  (loop for i below (ceiling (/ z 4 *ncpu* *tl-size*)) do
    (dolist (k
      (mapcar
        (function sb-thread:join-thread)
        (loop for j below *ncpu* collect
          (let ((j j))
            (sb-thread:make-thread
              (lambda ()
                (mapcar
                  (lambda (x)
                    ;(format t "~d ~d ~%" x (+ (* i *ncpu*) j))	;debug
                    (if (= 0 (mod z x))
                      t
                      nil))
                  (genl (+ (* i *ncpu*) j) *tl-size*))))))))
      ;(print k)	;debug
      (dolist (l k)
        (if l
        (return-from ch-brt-thread-old t))))))



;
;(defun prim-o (z)
;  (cond
;    ((< z 2) nil)
;    ((< z 4) t)
;    ((= 0 (mod z 2)) nil)
;    ((= 0 (mod z 3)) nil)
;    ((= 0 (mod z 5)) nil)
;
;    ((loop for i below (ceiling (/ z 4 *ncpu* *tl-size*)) do
;      (dolist (k
;        (mapcar
;          (function sb-thread:join-thread)
;          (loop for j below *ncpu* collect
;            (let ((j j))
;              (sb-thread:make-thread
;                (lambda ()
;                  (mapcar
;                    (lambda (x)
;                      ;(format t "~d ~d ~%" x (+ (* i *ncpu*) j))	;debug
;                      (if (= 0 (mod z x))
;                        t
;                        nil))
;                    (genl (+ (* i *ncpu*) j) *tl-size*))))))))
;        ;(print k)	;debug
;        (dolist (l k)
;          (if l
;          (return-from prim-o nil))))))
;    (t t)))


(defun ch-brt-thread (z) 
  ;(loop for i below (ceiling (/ z 4 *ncpu* *tl-size*)) do
  (loop for i below (sqrt (ceiling (/ z 2 *ncpu* *tl-size*))) do
    (dolist (k
      (mapcar
        (function sb-thread:join-thread)
        (loop for j below *ncpu* collect
          (let ((j j))
            (sb-thread:make-thread
              (lambda ()
                (dolist (n
                  (mapcar
                    (lambda (x)
                      ;(format t "~d ~d ~%" x (+ (* i *ncpu*) j))	;debug
                      (if (= 0 (mod z x))
                        t
                        nil))
                    (genl (+ (* i *ncpu*) j) *tl-size*)))
                  (if n
                  (sb-thread:return-from-thread t)))))))))
      ;(print k)	;debug
      (if k
      (return-from ch-brt-thread t)))))


(defun prim (z)
  (cond
    ((< z 2) nil)
    ((< z 4) t)
    ((= 0 (mod z 2)) nil)
    ((= 0 (mod z 3)) nil)
    ((= 0 (mod z 5)) nil)

    ((if (> 1 (/ z 4 *ncpu* *tl-size*))
       (ch-brt-smpl z)
       (ch-brt-thread z))
     nil)
    (t t)))


;(loop for i from 0 to 109 do
;     (format t "ex-isprime(~a)= ~d~%" i (ex-isprime i)))

;(loop for i in test do
;     (format t "ex-isprime(~a)= ~d~%" i (time (ex-isprime i))))
 

;(loop for i from 0 to 109 do
;     (format t "ex-isprime(~a)= ~d~%" i (ex-isprime i)))
;(loop for i in *test* do
;    (format t "ex-isprime(~a)= ~d~%" i (time (ch-brt-smpl i))))

;(print "lllo")
;
;(print 
;  (let ((z 11))
;    (if (> 1 (/ z 4 *ncpu* *tl-size*))
;       (progn (print "small")(ch-brt-smpl z))
;       (progn (print "big")(ch-brt-thread z)))))
;(print 
;  (let ((z 10000))
;    (if (> 1 (/ z 4 *ncpu* *tl-size*))
;       (progn (print "small")(ch-brt-smpl z))
;       (progn (print "big")(ch-brt-thread z)))))
;(print 
;  (let ((z 10007))
;    (if (> 1 (/ z 4 *ncpu* *tl-size*))
;       (progn (print "small")(ch-brt-smpl z))
;       (progn (print "big")(ch-brt-thread z)))))


;(print (prim 10007))
;(loop for i in *test* do
;  (let ((x (time(ch-brt-smpl i)))
;	(y (time (ch-brt-thread i))))
;    (write-line "===========")
;    (format t "ch-brt-smpl(~a)= ~d~%ch-brt-thread(~a)= ~d~%" i x i y)
;    (write-line "===========")))

;(defun lt-size-benchmark ()
;  (print 1009140611)
;  (print *ncpu*)
;  (print *tl-size*)
;  (loop for i below 1000 do
;    (format t "grep ~d ~%" (+ 30000 (* i 1000)))
;    (set '*tl-size* (+ 1000 (* i 1000)))
;    (format t "CPU ~d" *tl-size*)
;    (print (time (ch-brt-thread 100914061)))))
;(lt-size-benchmark)

(loop for i in *test* do
     (format t "(prim ~a)= ~d~%" i (time (prim i))))
