#ifndef GPARSERES_H
#define GPARSERES_H

#include <QString>

class GParseRes
{
public:
    GParseRes(const QString &message);
    operator =(const QString &errMessage);
    bool isEmpty() const;
    QString message() const;
private:
    QString m_message;
};

#endif // GPARSERES_H
