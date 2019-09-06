#include <QCoreApplication>

#include <QDebug>
#include "gcolorcondition.h"

int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);

    GAbilityStream gas(" bronze     or    non-   silver or     gold or non-gold");

    return a.exec();
}
