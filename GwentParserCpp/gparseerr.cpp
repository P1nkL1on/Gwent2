#include "gparseerr.h"
#include <iostream>

GErr::GErr(const int streamStartPosition, const QString &message):
    m_errorMessage(message),
    m_streamPosition(streamStartPosition)
{
}

GErr::GErr(const int streamStartPosition, const int streamEndPosition, const QString &message):
    m_errorMessage(message),
    m_streamPosition(streamStartPosition),
    m_streamPositionEnd(streamEndPosition)
{
}

GErr &GErr::operator +=(const GErr &childError)
{
    m_causes << childError;
    return *this;
}


QStringList GErr::message() const
{
    QStringList message = QStringList() << m_errorMessage;
    foreach (const GErr& err, m_causes)
        message << err.message();
    return message;
}


bool GErr::isEmpty() const
{
    return m_errorMessage.isEmpty() && m_causes.isEmpty();
}

int GErr::pos() const
{
    return m_streamPosition;
}

bool GErr::show(const GAbilityStream &stream) const
{
    using namespace std;
    if (isEmpty()){
        cout << "success" << endl;
        return true;
    }
    cout << m_errorMessage.toStdString();
    if (m_errorMessage[m_errorMessage.length() - 1] == '!')
        cout << "\t\t" << stream.wordsAround(m_streamPosition).toStdString();
    cout << endl;
    foreach (const GErr& err, m_causes)
        err.show(stream);
    return false;
}
