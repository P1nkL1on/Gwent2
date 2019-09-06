#include <QCoreApplication>

#include <QDebug>
#include "gunit.h"

int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);

    GAbilityStream gas(" bronze     or    non-   silver or     gold or non-gold.");
    GColorCondition gc;
    qDebug() << gc.parseFrom(gas);
    qDebug() << gc.toString();

    return a.exec();
}
