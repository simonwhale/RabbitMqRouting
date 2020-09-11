# RabbitMqRouting

This is a learning project of where I am looking at the routing of messages through a RabbitMq Exchange.  It is split into 3 projects

# The Emitter
This project has 2 end point, one for making a "warning" message and the other will create an "error" message

# Read Warnings
This project will only pull messages that have a warning, routing key

# Read Errors
This project will only pull messages that have a error, routing key


#Notes
This project assumes that you have rabbitMq installed, I currently have this working by using a docker image of RabbitMq
