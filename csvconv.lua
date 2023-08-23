-- Function to split a string into a table based on a delimiter
function split(str, delimiter)
    local result = {}
    for match in (str..delimiter):gmatch("(.-)"..delimiter) do
        table.insert(result, match)
    end
    return result
end

-- Read the CSV file and process the data
function processCSV(inputFilename, outputFilename)
    local input = io.open(inputFilename, "r")
    local output = io.open(outputFilename, "w")
    local translations = {}

    for line in input:lines() do
        local fields = split(line, ",")
        local russian = fields[1]
        local translation = fields[4]
        
        if russian and translation and translation ~= "" then
            if not translations[translation] then
                translations[translation] = true
                output:write(line .. "\n")
            end
        else
            output:write(line .. "\n")
        end
    end

    input:close()
    output:close()
end

-- Define input and output filenames
local inputFilename = "favourites2023-08-23-02-45-47.csv"
local outputFilename = "favouritesConverted.csv"

-- Process the CSV file
processCSV(inputFilename, outputFilename)
print("Processing complete. Output written to " .. outputFilename)
