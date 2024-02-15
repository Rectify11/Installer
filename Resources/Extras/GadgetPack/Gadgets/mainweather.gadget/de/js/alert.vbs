function confirm(prompt)
      dim res
      res = MsgBox (prompt, 48, "Wetter Gadget")
      if res=1 then
          confirm = true
      else
          confirm = false
      end if
end function