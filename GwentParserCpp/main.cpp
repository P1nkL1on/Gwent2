#include <QCoreApplication>

#include "gerrorhandler.h"
#include "gunit.h"

int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);

    GAbilityStream gas("bronze unit");
    GUnit gc;

    if (GErrorHandler::show(gc.parseFrom(gas)))
        std::cout << std::endl << gc.toString().toStdString() << std::endl;

    return a.exec();
}
