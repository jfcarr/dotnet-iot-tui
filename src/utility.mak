# To use this make file:
#   1. Create a copy called 'Makefile'
#   2. Update the PI_SERVER and PI_USER variables to match your configuration.
#   3. Run 'make'.

PI_SERVER = change_to_raspberry_pi_hostname
PI_USER = change_to_raspberry_pi_username
ARCHIVE_NAME = SenseHatProviders.zip
REST_SERVICE = SenseHatProvider
GRPC_SERVICE = SenseHatGrpcProvider
FLASK_SERVICE = SenseHatFlaskProvider
SHARED_LIB = SenseHatLib
TARGET = services
VNC_COMMAND = vncserver-virtual

default:
	@echo 'Copy to, and access, the Raspberry Pi:'
	@echo '  copy-pi    -- Bundle the service projects into a .zip file, and copy them to the Raspberry Pi.'
	@echo '  shell-pi   -- Use SSH to open a remote shell on the Raspberry Pi.'
	@echo ''
	@echo 'Pi commands (to be run directly on the Raspberry Pi server):'
	@echo '  deploy     -- Extract the project bundle.'
	@echo '  run-rest   -- Run the .NET REST (WebAPI) service.'
	@echo '  run-grpc   -- Run the .NET gRPC service.'
	@echo '  run-flask  -- Run the Flask/Python service.'
	@echo '  vnc-start  -- Start the VNC service.'
	@echo '  vnc-stop   -- Stop the VNC service.'

copy-pi:
	-rm -f $(ARCHIVE_NAME)
	zip -r $(ARCHIVE_NAME)  ./$(REST_SERVICE) ./$(GRPC_SERVICE) ./$(SHARED_LIB) ./$(FLASK_SERVICE)
	scp $(ARCHIVE_NAME) Makefile $(USER)@$(PI_SERVER):/home/$(USER)
	-rm -f $(ARCHIVE_NAME)

shell-pi:
	ssh $(PI_USER)@$(PI_SERVER)

deploy:
	mkdir -p $(TARGET)
	-rm -rf ./$(TARGET)/$(SHARED_LIB)
	-rm -rf ./$(TARGET)/$(REST_SERVICE)
	-rm -rf ./$(TARGET)/$(GRPC_SERVICE)
	-rm -rf ./$(TARGET)/$(FLASK_SERVICE)
	unzip ./$(ARCHIVE_NAME) -d ./$(TARGET)

run-rest:
	dotnet run --project ./$(TARGET)/$(REST_SERVICE)/$(REST_SERVICE).csproj

run-grpc:
	dotnet run --project ./$(TARGET)/$(GRPC_SERVICE)/$(GRPC_SERVICE).csproj

run-flask:
	flask --app ./services/$(FLASK_SERVICE)/service run --host=0.0.0.0 --port 5191

vnc-start:
	$(VNC_COMMAND)

vnc-stop:
	$(VNC_COMMAND) -kill :1
