#ifndef GERR_H
#define GERR_H

#include <QList>
#include <QString>
#include "gabilitystream.h"

class GErr
{
public:
    GErr() = default;
    GErr(const int streamPosition, const QString &message);
    GErr(const int streamStartPosition, const int streamEndPosition, const QString &message);
    GErr& operator +=(const GErr &childError);
//    GErr (const QString& message);

    QStringList message() const;
    bool isEmpty() const;
    int pos() const;
    bool show(const GAbilityStream &stream) const;
protected:
    QString m_errorMessage = QString();
    int m_streamPosition = -1;
    int m_streamPositionEnd = -1;
    QList<GErr> m_causes;
};

#endif // GERR_H
