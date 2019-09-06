#include "gtag.h"

GParse *GTag::createNew() const
{
    GTag* cop = new GTag();
    cop->m_tag = m_tag;
    return cop;
}

QString GTag::parseFrom(GAbilityStream &stream)
{
    QString currentTag = stream.nextWord();
    int words = 1;
    do{
        const int index = m_tags.indexOf(currentTag);
        if (index < 0){
            currentTag += QString(" %1").arg(stream.nextWord());
            ++words;
            continue;
        }
        m_tag = Tag(index);
        return QString();
    }while(words < 3 && !stream.end());
    return QString("'%1' is not a tag!").arg(currentTag.split(' ').first());
}

QString GTag::toString() const
{
    return toStringEnum(m_tags, "tag", m_tag);
}
