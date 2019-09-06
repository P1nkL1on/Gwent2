#include "gparseres.h"


GParseRes::GParseRes(const QString &errMessage, const bool isFatal)
{
    m_fatal = isFatal;
    m_message = errMessage;
}

GParseRes::operator =(const QString &errMessage)
{
    m_message = errMessage;
}

bool GParseRes::isEmpty() const
{
    return m_message.isEmpty();
}

bool GParseRes::isFatal() const
{
    return m_fatal;
}

QString GParseRes::message() const
{
    return m_message;
}
