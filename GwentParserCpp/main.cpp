#include <QCoreApplication>

#include "gerrorhandler.h"
#include "gunit.h"

int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);

    GAbilityStream gas("Bronze or non-silver Machine or Kaedwen enemy unit in graveyard and hand and hand and deck");
    GUnit gc;

    if (GErrorHandler::show(gc.parseFrom(gas)))
        std::cout << gc.toString().toStdString() << std::endl;

    return a.exec();
}
