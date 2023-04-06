# StringParcer
Реализация задания по синтаксическому анализу описания типов языка Modula-2.
Примеры правильных цепочек
TYPE er = SET OF CARDINAL;
TYPE GF = ARRAY [7..78], [98..566] OF CHAR;
TYPE G = POINTER TO gvl;
TYPE gvf = RECORD g : INTEGER, FD : BOOLEAN END;
TYPE FR = [1 .. 25];
TYPE YGTFRE = (6, 3, 2, 4, 1, 980);
Если есть ошибка в строке ввода, программа выделяет ее местоположение. После корректного выполнения в левую таблицу заносятся имена всех идентификаторов и необходимый размер памяти под них, а в правую таблицу – все значения использованных констант.
