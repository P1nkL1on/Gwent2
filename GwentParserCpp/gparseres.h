#ifndef GPARSERES_H
#define GPARSERES_H

#include <QString>

class GParseRes
{
public:
    GParseRes(const QString &errMessage, const bool isFatal = false);
    operator =(const QString &errMessage);
    bool isEmpty() const;
    bool isFatal() const;
    QString message() const;
private:
    bool m_fatal = true;
    QString m_message;
};

#endif // GPARSERES_H
