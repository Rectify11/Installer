        ��  ��                  	  ,   U I F I L E   ���    0            <duixml>

#pragma once

<stylesheets>

<style resid="ModernStyle">

<if class="ProgessText">

<RichText

font          = "iconfont;200%;semibold"

foreground    = "gtc(DirectUI::ItemsView, 0, 0, 3803)"

contentalign  = "middleleft | wrapleft | endellipsis"

accessible    = "true"

accRole       = "statictext"/>

</if>

</style>

</stylesheets>

<Element resid="main" id= "atom(main)" layout="verticalflowlayout(1, 2, 2, 2)" layoutpos="client" padding="rect(0rp, 10rp, 48rp, 0rp)" sheet="ModernStyle" background ="gtc(DirectUI::ItemsView, 0, 0, 3802)">

<ModernProgressRing

id          = "atom(ModernProgressRing)"

width       = "60rp"

height      = "60rp"

accname     = "Progress indicator"

accrole     = "ProgressBar"

visible     = "true"

foreground  = "gtc(DirectUI::ItemsView, 0, 0, 3803)"/>

<RichText id          = "atom(ProgressText)"

content     = "resstr(102)"

class       = "ProgessText"

margin      = "rect(10rp, 8rp, 0rp, 0rp)"/>

</Element>

</duixml>
   