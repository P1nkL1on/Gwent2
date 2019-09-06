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
    foreach (const QString &string, list){
        cout << QString("%1").arg(string == list.first()? "" : sep).toStdString();
        const QStringList parts = string.split(" and ");
        foreach (const QString &part, parts){
            cout << part.toStdString();
            if (parts.last() != part) cout
                    << endl << "    and ";
        }
        //errorMessage
        cout << endl;
    }
    return false;
}
