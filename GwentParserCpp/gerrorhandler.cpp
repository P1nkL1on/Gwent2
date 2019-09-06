#include "gerrorhandler.h"
#include <QStringList>

using namespace std;

bool GErrorHandler::show(const GParseRes &operationRes)
{
    if (operationRes.isEmpty()){
        cout << "Parsing success" << endl;
        return true;
    }
    const QString sep = "because ";
    const QStringList list = operationRes.message().split(sep);
    foreach (const QString &string, list)
        cout << QString("%2%1").arg(string).arg(string == list.first()? "" : sep).toStdString() << endl;
    return false;
}
