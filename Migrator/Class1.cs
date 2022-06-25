from sense_hat import SenseHat
import time

s = SenseHat()
s.low_light = False

green = (0, 255, 0)
yellow = (255, 255, 0)
blue = (0, 0, 255)
red = (255, 0, 0)
white = (255, 255, 255)
nothing = (0, 0, 0)
pink = (255, 105, 180)


def show(x1, x2):
  print("W1 " + str(x1) + " W2 " + str(x2))
  R = red
  O = nothing
  logo =[
    O, O, O, O, O, O, O, O,
    O, O, O, O, O, O, O, O,
    O, O, O, O, O, O, O, O,
    O, O, O, O, O, O, O, O,
    O, O, O, O, O, O, O, O,
    O, O, O, O, O, O, O, O,
    O, O, O, O, O, O, O, O,
    O, O, O, O, O, O, O, O,
    ]


  logo[x1] = R
  logo[x1 + 1] = R
  logo[x2] = R
  logo[x2 + 1] = R


  return logo


def kat(x, y, z, starex, starey):
    
  global ball, movex, movey

 
  if(x-starex<0):
    ball -= 8
    movex = False

  if (x - starex > 0):
    ball += 8
    movex = True


  if (x - starex == 0):
    if movex == True:
      ball += 8

    elif movex==False:
      ball -= 8



  if (y - starey > 0):
    ball += 1
    movey = True


  if (y - starey < 0):
    ball -= 1
    movey = False


  if (y - starey == 0):
    if movey == True:
      ball += 1


    elif movey==False:
      ball -= 1
  check()



def check():
  global ball
  print("checking")
  gora =[0, 1, 2, 3, 4, 5, 6, 7]
  dol =[56, 57, 58, 59, 60, 61, 62]
  lewo =[0, 8, 16, 24, 32, 40, 48, 56]
  prawo =[7, 15, 23, 31, 39, 40, 47, 55, 63]
  for item in gora:
    if ball - 8 == item:
      print("too high")
      ball += 8
  for item in dol:
    if ball == item:
      print("too down")
      ball -= 8
  for item in lewo:
    if ball == item:
      print("too left")
      ball += 1
  for item in prawo:
    if ball + 1 == item:
      print("too right")
      ball -= 1

movex = True
movey = False
starex = 0
starey = 0
ball = 35


while True: 
    
    orientation = s.get_orientation() #próbkowanie
    p = orientation["pitch"]
    r = orientation["roll"]
    y = orientation["yaw"]
    print("p: %s, r: %s, y: %s" % (p, r, y))
    time.sleep(1)
    kat(p, r, y, starex, starey)
    s.set_pixels(show(ball - 8, ball))
    time.sleep(.10)
    #zmiana nachylenia
    starex = p
    starey = r