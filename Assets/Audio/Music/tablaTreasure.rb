use_bpm 110


live_loop :ambient do
  use_synth :pretty_bell
  1.times do
    play :e4
    sleep 4
  end
  1.times do
    play :d4
    sleep 4
  end
end



live_loop :bd do
  sample :drum_bass_soft, release:0
  sleep 1
  sample :drum_bass_soft, release:0
  sleep 1
  sample :drum_bass_soft, release:0
  sleep 1.5
  sample :drum_bass_soft, release:0
  sleep 2.5
  #sample :drum_bass_soft, release:0
  #sleep 1
  sample :drum_bass_soft, release:0
  sleep 2
  
end


live_loop :tabla_ke do
  sample :tabla_ke3
  sleep 0.5
  
  if one_in(2)
    3.times do
      sample :tabla_ke3
      sleep 0.166666
    end
    sample :tabla_ke3
    sleep 0.5
    sample :tabla_ke3
    sleep 1.5
  else
    sleep 0.5
    sample :tabla_ke3
    sleep 2
  end
  
  sample :tabla_ke3
  sleep 2
  sample :tabla_ke3
  sleep 1
  sample :tabla_ke3
  sleep 2
end

live_loop :tabla_te do
  sleep 1
  if one_in(2)
    sample :tabla_te1
    sleep 1
  else
    sleep 1
  end
  sleep 6
end
