#include <QCoreApplication>

#include <iostream>
#include "gerrorhandler.h"
#include "gunit.h"

int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);
    QStringList tests = QStringList()
            << "bronze unit"
            << "unit"
            << "bronze or gold unit"
            << "gold cursed unit"
            << "non-gold unit"
            << "non-gold non-cursed or non-warrior unit"
            << "cursed gold unit in hand"
            << "unit in hand"
            << "unit in hand and and hand"
            << "unit in hand and deck and hand and deck and graveyard"
            << "unit in deck, hand or hand"
            << "gold in deck"
            << "bronze soldier or aedirn or kaedwen or beast ally units"
            << "ally or enemy unit deck"
            << "unit hand"
            << "bronze self"
            << "bronze units"
            << "bronze unit in hand and deck"
               ;
    foreach (const QString &test, tests){
        GAbilityStream gas(test);
        GUnit gc;

        std::cout << "\n\n\n___" << test.toStdString() << "___" << std::endl;
        if (gc.parseFrom(gas).show(gas))
            std::cout << gc.toString().toStdString() << std::endl;

    }
    return a.exec();
}
