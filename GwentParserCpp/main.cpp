#include <QCoreApplication>

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
               ;
    foreach (const QString &test, tests){
        GAbilityStream gas(test);
        GUnit gc;

        std::cout << std::endl << "___" << test.toStdString() << "___: " << std::endl;
        if (GErrorHandler::show(gc.parseFrom(gas)))
            std::cout << gc.toString().toStdString() << std::endl;

    }
    return a.exec();
}
