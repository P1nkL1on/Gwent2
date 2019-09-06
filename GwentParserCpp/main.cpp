#include <QCoreApplication>

#include "gerrorhandler.h"
#include "gunit.h"

int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);

    GAbilityStream gas("bronze non-spying ally unit in deck");
    GUnit gc;

    if (GErrorHandler::show(gc.parseFrom(gas)))
        std::cout << gc.toString().toStdString() << std::endl;

    return a.exec();
}
