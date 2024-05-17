*Опис на играта

Играта е базирана на класичната игра Minesweeper. Составена е од едноставна табла со покриени полиња коишто може да бидат празни или да содржат мина. Целта на играчот е да ги отвори сите полиња без да погоди мина. Доколку погоди мина играта завршува. Со секое успешно отворање на поле играчот собира поени. Оваа имплементација е составена од три различни нивоа на тежина кои може да се изберат со кликање на менито Select difficulty. Во зависност од нивото на тежина поените се пресметуваат различно и може да истече времето на игра. 

Упатство: Со лев клик на глувчето отворате поле на кое може да се појави бројче коешто означува колку мини има околу него. Со десен клик од глувчето поставувате знаменце и потоа не може да го отворите полето доколку не го отстраните знамето повторно со десен клик. Доколку успешно ја завршите играта или пак погодите мина, може да почнете од почеток со кликање на копчето за рестарт на средина од горниот дел на прозорецот.

![Screenshot 2024-05-17 230912](https://github.com/martinanajdovska/Minesweeper_VP/assets/136980739/9cb5efa0-1e1a-4538-b1bb-77d2ea6ef5bd) 
![Screenshot 2024-05-17 230923](https://github.com/martinanajdovska/Minesweeper_VP/assets/136980739/435d5c09-09eb-43fb-aa73-3595fc65778c)
![Screenshot 2024-05-17 231026](https://github.com/martinanajdovska/Minesweeper_VP/assets/136980739/fac500ec-745a-4e3e-b2f7-fe416b17f331)

*Решение на проблемот

Сите податоци за играта се чуваат во почетната класа за формата. Потребни променливи се бројот на редици и колони од полиња, бројот на мини, тежината, колку полиња се отворени, колку знаменца се поставени, колку време е изминато од почетокот на играта, колку вкупно празни полиња постојат и низа од копчиња. Со почеток на играта веднаш се отвора Easy ниво на тежина и имаме можност да почнеме да играме. За секое ниво на тежина постојат одредени вредности за бројот на редици и колони и бројот на мини. Позициите на мините се одредени со Random генератор на позиции. Исто така се чува и High Score за сесијата на играчот користејќи Properties на формата. За секоја од методите од решението е поставено детално објаснување преку коментари во рамки на кодот.

![Screenshot 2024-05-17 233934](https://github.com/martinanajdovska/Minesweeper_VP/assets/136980739/fd5bccd1-c480-43b3-9ebe-ac84bb689e73)


*Опис на имплементација на некои од методите

** CheckForMine методот се повикува при секој лев клик на играчот на неотворено поле. Методот најпрво проверува дали кликнатото поле има знаменце, доколку има не прави ништо бидејќи означените полиња не смеат да се отвораат. Доколку нема знаме и полето е празно полето се отвора или пак доколку овој клик претставува првиот клик од почетокот на играта, полето се отвора без разлика дали имало мина на него за првиот клик да не биде истовремено и крај на играта. Потоа се повикува метод CountNeighbourMines за проверка на тоа колку мини има околу кликнатото поле за на полето да се испише соодветниот број. Доколку пак на кликнатото поле имало мина, се повикува методот GameOver којшто ја прекинува играта.

![Screenshot 2024-05-17 233301](https://github.com/martinanajdovska/Minesweeper_VP/assets/136980739/3fc8b0b0-be68-4703-ae12-0c06019c3121)


** CountNeighbourMines методот е составен од повеќе чекори. Најпрво има вгнезден циклус со којшто се проверува колкав е бројот на мини околу соодветното поле. Доколку околу соодветното поле има барем една мина или знаме, полето се означува со бројот на мини околу него и потоа методот завршува. Но, доколку околу полето немало ниту мина, ниту знаменце тогаш се повикува метод GetEmptyNeighbours којшто во листа ги сместува сите соседни полиња коишто се исто така празни и за секое од нив го повикува методот CountNeighbourMines. На овој начин одеднаш се отвораат сите соседни полиња коишто се празни се додека не се стигне до знаме или мина.

![Screenshot 2024-05-18 000102](https://github.com/martinanajdovska/Minesweeper_VP/assets/136980739/727bf24b-fa85-4c1f-9406-48bfd1cbefbf)
![Screenshot 2024-05-17 233428](https://github.com/martinanajdovska/Minesweeper_VP/assets/136980739/d3b0cc9e-f2bc-4ba2-9f8a-be4b3f0b00f0)



** GetEmptyNeighbours методот е составен од вгнезден циклус кој за даденото поле ги проверува сите негови соседни полиња дали се празни и ги додава во листа. Доколку некое од полињата претходно било отворено тоа се прескокнува за да не влезе во бесконечен циклус кадешто истите полиња ќе се додаваат во листа постојано.

![Screenshot 2024-05-17 233503](https://github.com/martinanajdovska/Minesweeper_VP/assets/136980739/93486ecf-5a06-4674-a351-872b39cf4b72)

** GameOver методот е повикан кога играта ќе заврши по погодување на мина, истекување на времето или успешно отворање на сите полиња. Методот ги исклучува сите копчиња за играчот да не може да продолжи и потоа проверува дали неговите поени се повисоки од дотогашниот High Score и соодветно ги зачувува новите поени доколку е потребно.

![Screenshot 2024-05-17 234222](https://github.com/martinanajdovska/Minesweeper_VP/assets/136980739/98ad2361-df47-4ae0-ac78-abcca3c813f9)

