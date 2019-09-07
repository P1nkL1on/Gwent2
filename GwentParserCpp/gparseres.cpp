#include "gparseres.h"


GParseRes::GParseRes(const QString &message)
{
    m_message = message;
}

GParseRes::operator =(const QString &errMessage)
{
    m_message = errMessage;
}

bool GParseRes::isEmpty() const
{
    return m_message.isEmpty();
}

QString GParseRes::message() const
{
    return m_message;
}
