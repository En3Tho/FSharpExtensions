namespace En3Tho.FSharp.Validation.Json

open System.Text.Json
open System.Text.Json.Serialization
open En3Tho.FSharp.Validation

type NewCtorValidatorValidatedJsonConverter<'value, 'validator when 'validator: (new: unit -> 'validator) and 'validator :> IValidator<'value>>() =
    inherit JsonConverter<NewCtorValidatorValidated<'value, 'validator>>()

    override this.Write(writer, value, options) =
        JsonSerializer.Serialize(writer, value.Value, options)

    override this.Read(reader, typeToConvert, options) =
        let value = JsonSerializer.Deserialize<'value>(&reader, options)
        NewCtorValidatorValidated<'value, 'validator>.Make(value)

type WriteOnlyNewCtorAsyncValidatorValidatedJsonConverter<'value, 'validator when 'validator: (new: unit -> 'validator) and 'validator :> IValidator<'value>>() =
    inherit JsonConverter<NewCtorAsyncValidatorValidated<'value, 'validator>>()

    override this.Write(writer, value, options) =
        JsonSerializer.Serialize(writer, value.Value, options)

    override this.Read(reader, typeToConvert, options) =
        invalidOp "NewCtorAsyncValidatorValidated value cannot be read from Json. Not supported."

type WriteOnlyInstanceValidatorValidatedJsonConverter<'value, 'validator when 'validator: (new: unit -> 'validator) and 'validator :> IValidator<'value>>() =
    inherit JsonConverter<InstanceValidatorValidated<'value, 'validator>>()

    override this.Write(writer, value, options) =
        JsonSerializer.Serialize(writer, value.Value, options)

    override this.Read(reader, typeToConvert, options) =
        invalidOp "InstanceValidatorValidated value cannot be read from Json. Not supported."

type WriteOnlyInstanceAsyncValidatorValidatedJsonConverter<'value, 'validator when 'validator: (new: unit -> 'validator) and 'validator :> IValidator<'value>>() =
    inherit JsonConverter<InstanceAsyncValidatorValidated<'value, 'validator>>()

    override this.Write(writer, value, options) =
        JsonSerializer.Serialize(writer, value.Value, options)

    override this.Read(reader, typeToConvert, options) =
        invalidOp "InstanceAsyncValidatorValidated value cannot be read from Json. Not supported."